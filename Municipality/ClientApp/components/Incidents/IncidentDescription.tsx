import * as React from 'react';
import { IIncident } from './logic/incidentsState';
import autobind from 'autobind-decorator';

interface IInnerProps {
    incident: IIncident;
    focusIncident: (id: number, value: boolean) => void;
}
export default class IncidentDescription extends React.Component<IInnerProps, any>{

    constructor(props: IInnerProps) {
        super(props);

    }
    @autobind
    FocusIncident() {

        this.props.focusIncident(this.props.incident.id, !this.props.incident.inFocus);
    }
    render() {
        return <tr  onClick={this.FocusIncident} className={this.props.incident.inFocus ? "desc-focus cursor":"cursor"}>
            <td>{this.props.incident.title}</td>           
            <td>{this.props.incident.adress}</td>   
        </tr>
    }

}