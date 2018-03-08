import * as React from 'react';
import autobind from 'autobind-decorator';

import { IIncident } from './logic/incidentsState';
import Carouse from '../Incidents/Carousel';

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
        //(this.refs.modal as HTMLButtonElement).click();

    }
    render() {
        if (this.props.incident.inFocus) {
            (this.refs.modal as HTMLButtonElement).click();
        }
        return <tr className={this.props.incident.inFocus ? "desc-focus cursor" : "cursor"}>
            <td onClick={this.FocusIncident}>
                {this.props.incident.title}

            </td>
            <td onClick={this.FocusIncident}>
                {this.props.incident.adress}
            </td>
            <td>
                <button ref='modal' type="button" className="btn btn-info btn-lg hide" data-toggle="modal" data-target={`#${this.props.incident.id}`}>Open Modal</button>

                <div className="modal open in " id={`${this.props.incident.id}`} role="dialog">
                    <div className="modal-dialog">

                        <div className="modal-content">
                            <div className="modal-header ">
                                <button type="button" className="close" data-dismiss="modal" onClick={this.FocusIncident}>&times;</button>
                                <h4 className="modal-title danger">{this.props.incident.title}</h4>
                                <h5><strong>Times remain: </strong>{this.props.incident.timeLeft} hours</h5>
                            </div>
                            <div className="modal-body">
                                <p>{this.props.incident.description}</p>
                                <p className="modal-description">{this.props.incident.adress}</p>
                                <Carouse
                                    incident={this.props.incident}
                                />
                            </div>
                                <div className="modal-footer">
                                    <button type="button" className="btn btn-danger" data-dismiss="modal" onClick={this.FocusIncident} >Close</button>
                                </div>
                            </div>

                        </div>
                    </div>
            </td>
        </tr>
            }

}