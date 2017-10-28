import * as React from 'react';
import { IIncident } from './logic/adminState';
import autobind from 'autobind-decorator';

interface IInnerProps {
    incident: IIncident;
    approveIncident: (id: number) => void;
    forbidIncident: (id: number) => void;
}

export default class Incident extends React.Component<IInnerProps, any> {

    @autobind
    Approve(event: React.FormEvent<HTMLButtonElement>) {
        event.currentTarget.blur();

        this.props.approveIncident(this.props.incident.id);
    }
    @autobind
    Forbid(event: React.FormEvent<HTMLButtonElement>) {
        event.currentTarget.blur();

        this.props.forbidIncident(this.props.incident.id);
    }

    render() {
        return <tr >
            <td>{this.props.incident.title}</td>
            <td>{this.props.incident.description}</td>
            <td>{this.props.incident.adress}</td>
            <td><button type="button" className="btn btn-primary" onClick={this.Approve}>Approve</button></td>
            <td><button type="button" className="btn btn-danger" onClick={this.Forbid}>Forbid</button></td>
            <td>

                <button ref='modal' type="button" className="btn btn-info" data-toggle="modal" data-target={`#${this.props.incident.id}`}>Show</button>

                <div className="modal open in " id={`${this.props.incident.id}`} role="dialog">
                    <div className="modal-dialog">

                        <div className="modal-content">
                            <div className="modal-header ">
                                <button type="button" className="close" data-dismiss="modal">&times;</button>
                                <h4 className="modal-title danger">{this.props.incident.title}</h4>
                            </div>
                            <div className="modal-body">
                                <p>{this.props.incident.description}</p>
                                <p className="modal-description">{this.props.incident.adress}</p>
                                <img src={this.props.incident.filePath} className="img-responsive" alt="/" />
                            </div>
                            <div className="modal-footer">
                                <button type="button" className="btn btn-success" data-dismiss="modal" onClick={this.Approve}>Approve</button>
                                <button type="button" className="btn btn-default" data-dismiss="modal" onClick={this.Forbid}>Forbid</button>
                                <button type="button" className="btn btn-danger" data-dismiss="modal">Close</button>
                            </div>
                        </div>

                    </div>
                </div>
            </td>

        </tr>
    };

}