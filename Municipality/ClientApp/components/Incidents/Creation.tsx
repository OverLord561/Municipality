import * as React from 'react';
import autobind from 'autobind-decorator';
import axios from 'axios';

import { GeolocatedProps, geolocated } from 'react-geolocated';

interface IInnerState {
    title: string;
    description: string;
    lat: any;
    lng: any;
    file: any;
    [key: string]:string;
   
}

 class Creation extends React.Component<any, IInnerState> {
    constructor(props: any) {
        super(props);
        this.state = {
            title: "",
            description: "",
            lat:  "",
            lng: "",
            file:""
        }
    }
    @autobind
    SetTitle(event: React.FormEvent<HTMLInputElement>) {
        this.setState({
            title: event.currentTarget.value
        });
    }

    @autobind
    SetDescription(event: React.FormEvent<HTMLInputElement>) {
        this.setState({
            description: event.currentTarget.value
        });
    }

    @autobind
    CreateIncident(event: React.FormEvent<HTMLFormElement>) {
        event.preventDefault();
        console.log(this.state)

        const config = { headers: { 'Content-Type': 'multipart/form-data' } };

        var incident = new FormData();

        var state: IInnerState = this.state;

        Object.keys(state).map(function (key) {
            console.log(key + state[key]);
            incident.append(key.toString(), state[key]);
        });

        console.log(incident)
       
      


        axios.post('/api/incident', incident, config)
            .then(response => {
                console.log(response)
        }).catch(error => {
            console.log(error);
        });
    }

    @autobind
    UploadFile(event:any) {
        var files = event.currentTarget.files;

        var data = new FormData();

        this.setState({
            file: files[0]
        });
        
        
    }
    componentWillReceiveProps(nextProps: any, nextState: IInnerState) {
        
        this.setState({
            lat: nextProps.coords && nextProps.coords.latitude,
            lng: nextProps.coords && nextProps.coords.longitude
        });
    }

    render() {
        return <div className="col-lg-6 block">           

            <form onSubmit={this.CreateIncident}>
                <input value={this.state.title} onChange={this.SetTitle} required placeholder="Enter title..." />
                <input value={this.state.description} onChange={this.SetDescription} required placeholder="Enter description..." />

               
                <label htmlFor="attach-button" className="cursor attach">Attach File</label>
                <input onChange={this.UploadFile} className="hide" id="attach-button" type="file" accept="image/*" />


                <input value={this.state.lat} />
                <input value={this.state.lng} />
                
                <input type="submit" value="Send" />
            </form>

        </div>

    }
}

 export default geolocated()(Creation);
