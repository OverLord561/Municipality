import * as React from 'react';
import { IIncident } from './logic/adminState';
import autobind from 'autobind-decorator';

interface IInnerProps {
    incident: IIncident;
    approveIncident: (incident: IIncident) => void;
    forbidIncident: (id: number) => void;
}

interface IInnerState {
    estimate: number;
    priorityId: number;
    priority: string;
}

export default class Incident extends React.Component<IInnerProps, IInnerState> {


    constructor(props: IInnerProps) {
        super(props);
        this.state = {
            estimate: this.props.incident.estimate,
            priorityId: this.props.incident.priorityId,
            priority: this.props.incident.priority
        }
    };
    @autobind
    Approve(event: React.FormEvent<HTMLButtonElement>) {
        event.currentTarget.blur();

        var incident: IIncident = { ...this.props.incident };

        incident.estimate = this.state.estimate;
        incident.priorityId = this.state.priorityId;
        incident.priority = this.state.priority;

        this.props.approveIncident(incident);
    };
    @autobind
    Forbid(event: React.FormEvent<HTMLButtonElement>) {
        event.currentTarget.blur();

        this.props.forbidIncident(this.props.incident.id);
    };
    @autobind
    SetEstimate(event: React.FormEvent<HTMLInputElement>) {

        this.setState({
            estimate: +event.currentTarget.value
        });
    };
    @autobind
    SetPriority(event: any) {
        event.preventDefault();
        this.setState({
            priorityId: +event.currentTarget.dataset['id'],
            priority: event.currentTarget.innerText
        });
    };

    render() {
        return <tr >
            <td>{this.props.incident.title}</td>
            <td>{this.props.incident.description}</td>
            <td>{this.props.incident.adress}</td>
            
            <td><button type="button" className="btn btn-danger" onClick={this.Forbid}>Forbid</button></td>
            <td>

                <button ref='modal' type="button" className="btn btn-primary" data-toggle="modal" data-target={`#${this.props.incident.id}`}>Approve</button>

                <div className="modal open in " id={`${this.props.incident.id}`} role="dialog">
                    <div className="modal-dialog">

                        <div className="modal-content">
                            <div className="modal-header text-center">

                                <button type="button" className="close" data-dismiss="modal">&times;</button>

                                <h4 className="modal-title danger inline-block">{this.props.incident.title}</h4>

                                <div className="dropdown inline-block">
                                    <button className="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown">{this.state.priority == "" ? "set priority ! ":this.state.priority}
                                        <span className="caret"></span>
                                    </button>
                                    <ul className="dropdown-menu">
                                        <li><a data-id="2" className="cursor" onClick={this.SetPriority}>LOW</a></li>
                                        <li><a data-id="3" className="cursor" onClick={this.SetPriority}>MEDIUM</a></li>
                                        <li><a data-id="4" className="cursor" onClick={this.SetPriority}>HIGH</a></li>
                                    </ul>
                                </div>

                            </div>
                            <div className="modal-body">
                                <p>{this.props.incident.description}</p>
                                <p className="modal-description">{this.props.incident.adress}</p>
                                <img src={this.props.incident.filePath} className="img-responsive" alt="/" />
                                <br/>
                                <div className="form-group">
                                    <label htmlFor="estimate">Estimate: hours</label>
                                    <input type="text" required className="form-control" id="estimate" placeholder="Enter estimate..." value={this.state.estimate} onChange={this.SetEstimate} />
                                </div>
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