import * as React from 'react';
import { IIncident } from './logic/adminState';

interface IInnerProps {
    incident: IIncident;
}

export default class Incident extends React.Component<IInnerProps, any> {
    render() {
        return <tr >
            <td>{this.props.incident.title}</td>
            <td>{this.props.incident.description}</td>
            <td>{this.props.incident.adress}</td>
            <td><button type="button" className="btn btn-primary">Approve</button></td>
            <td><button type="button" className="btn btn-danger">Forbid</button></td>
        </tr>
    };

}