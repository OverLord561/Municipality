import * as React from 'react';
import autobind from 'autobind-decorator';
import axios from 'axios';
import { IIncident, IPoint } from './logic/incidentsState';
import { GeolocatedProps, geolocated } from 'react-geolocated';


interface IInnerState {
    title: string;
    description: string;
    lat: any;
    lng: any;
    file: any;  
    [key: string]: string;

}

interface IInnerProps {
    createIncident: (incident: FormData) => void;   
}

class Creation extends React.Component<IInnerProps, IInnerState> {
    constructor(props: IInnerProps) {
        super(props);
        this.state = {
            title: "title",
            description: "description",
            lat: "0",
            lng: "0",
            file: "",
            fileName:"no file attached"
        }
    }
    @autobind
    SetTitle(event: React.FormEvent<HTMLInputElement>) {
        this.setState({
            title: event.currentTarget.value
        });
    }

    @autobind
    SetDescription(event: React.FormEvent<HTMLTextAreaElement>) {
        this.setState({
            description: event.currentTarget.value
        });
    }

    @autobind
    CreateIncident(event: React.FormEvent<HTMLFormElement>) {
       
        event.preventDefault();

        var incident = new FormData();     
      

        var state: IInnerState = this.state;

        Object.keys(state).map(function (key) {           
            incident.append(key.toString(), state[key]);
        });
       
        this.props.createIncident(incident);

    }

    @autobind
    UploadFile(event: any) {
        var files = event.currentTarget.files;
        
        console.log(files[0])
        this.setState({
            file: files[0],
            fileName: files[0].name
        });


    }
    componentWillReceiveProps(nextProps: any, nextState: IInnerState) {

        this.setState({
            lat: nextProps.coords && nextProps.coords.latitude,
            lng: nextProps.coords && nextProps.coords.longitude
        });
    }

    render() {
        return <form onSubmit={this.CreateIncident}>
            <div className="form-group">
                <label htmlFor="title">Title:*</label>
                <input className="form-control" value={this.state.title} required onChange={this.SetTitle} id="title" placeholder="Enter title..." />
            </div>

            <div className="form-group">
                <label htmlFor="desc">Description:*</label>
                <textarea type="email" className="form-control" value={this.state.description} required onChange={this.SetDescription} id="desc" placeholder="Enter description..." />
            </div>

            <div className="form-group">
                <label htmlFor="attach-button" className="cursor attach">Attach File*</label>
                <input onChange={this.UploadFile} className="hide" id="attach-button" type="file" accept="image/*" required />
                <label className="file-name"> {this.state.fileName} </label>
            </div>

            <input required className="" disabled value={this.state.lat} />
            <input required className="" disabled value={this.state.lng} />

            <button type="submit" className="btn btn-default">Create</button>
        </form>;
        
    }
}

export default geolocated()(Creation);
