import * as React from 'react';
import { IIncident } from './logic/incidentsState'

interface IInnerProps {
    incident: IIncident;
    lat: any;
    lng: any;
}
export default class Incident extends React.Component<IInnerProps, any>{

    render() {
        return <div className="elem">{this.props.incident.title}</div>;
    }

}