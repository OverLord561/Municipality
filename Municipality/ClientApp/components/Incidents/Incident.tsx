import * as React from 'react';
import { IIncident } from './logic/incidentsState';
import autobind from 'autobind-decorator';

interface IInnerProps {
    incident: IIncident;
    lat: any;
    lng: any;
    focusIncident: (id: number, value: boolean) => void;
}
export default class Incident extends React.Component<IInnerProps, any>{

    constructor(props: IInnerProps) {
        super(props);

    }

    @autobind
    FocusIncident() {

        this.props.focusIncident(this.props.incident.id, !this.props.incident.inFocus);
    }
    render() {

        return <div onClick={this.FocusIncident} className="marker">
            <div className={this.props.incident.inFocus ? "image focus" : "image"}>
                <div className="title"> 1 </div>
            </div>
        </div >;
    }

}