import * as React from 'react';
import { GeolocatedProps, geolocated } from 'react-geolocated';
import autobind from 'autobind-decorator';

import { IIncident, IPoint, getIncidentCreateModel } from './logic/incidentsState';


interface IInnerProps {
    createIncident: (incident: IIncident, callback: Function) => void;
}

class Creation extends React.Component<IInnerProps, IIncident> {
    constructor(props: IInnerProps) {
        super(props);
        this.state = getIncidentCreateModel();
    }

    componentWillReceiveProps(nextProps: any, nextState: IIncident) {

        this.setState({
            lat: nextProps.coords && nextProps.coords.latitude,
            lng: nextProps.coords && nextProps.coords.longitude
        });
    }

    @autobind
    setTitle(event: React.FormEvent<HTMLInputElement>) {
        this.setState({
            title: event.currentTarget.value
        });
    }

    @autobind
    setDescription(event: React.FormEvent<HTMLTextAreaElement>) {
        this.setState({
            description: event.currentTarget.value
        });
    }

    @autobind
    createIncident(event: React.FormEvent<HTMLFormElement>) {

        event.preventDefault();

        this.props.createIncident(this.state, () => {
            (this.refs.form as HTMLFormElement).reset(); //to reset file attachment
        });

    }

    @autobind
    uploadFile(event: any) {
        this.setState({
            files: event.currentTarget.files
        });

    }
    

    render() {
        return <form ref="form" onSubmit={this.createIncident}>
            <div className="form-group">
                <label htmlFor="title">Title:*</label>
                <input className="form-control" value={this.state.title} required onChange={this.setTitle} id="title" placeholder="Enter title..." />
            </div>

            <div className="form-group">
                <label htmlFor="desc">Description:*</label>
                <textarea type="email" className="form-control" value={this.state.description} required onChange={this.setDescription} id="desc" placeholder="Enter description..." />
            </div>

            <div className="form-group">
                <label htmlFor="attach-button" className="cursor attach">Attach File*</label>
                <input onChange={this.uploadFile} className="hide" id="attach-button" type="file" accept="image/*" required />
                <label className="file-name"> {this.state.fileName} </label>
            </div>

            <input required className="" disabled value={this.state.lat} />
            <input required className="" disabled value={this.state.lng} />

            <button type="submit" className="btn btn-default">Create</button>
        </form>;

    }
}

export default geolocated()(Creation);
