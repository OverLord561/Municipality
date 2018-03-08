import * as React from 'react';
import { IState } from './logic/adminState';
import { IIncident } from '../Incidents/logic/incidentsState';
import Incident from './Incident';


interface IInnerProps {
    getNotApproved: () => void;
    notApprovedIncidents: IIncident[];
    approveIncident: (incident: IIncident) => void;
    forbidIncident: (id: number) => void;
}


export default class NotApproved extends React.Component<IInnerProps, any>{
    componentDidMount() {
       this.props.getNotApproved();
    }
    render() {
        var _props: IInnerProps = this.props;
        return <table className='table'>
            <thead>
                <tr>
                    <th>Title</th>
                    <th>Description</th>
                    <th>Adress</th>
                    <th></th>
                    <th></th>                    
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td><input placeholder = "Enter title..." type="text" className="form-control" /></td>
                    <td><input placeholder="Enter description..." type="text" className="form-control" /></td>
                    <td><input placeholder="Enter adress..." type="text" className="form-control" /></td>

                    <td><button type="button" className="btn btn-info">Search</button></td>
                    <td></td>
                   
                </tr>
                {this.props.notApprovedIncidents != undefined && this.props.notApprovedIncidents.map(function (incident, index) {
                    return <Incident
                        key={index}
                        incident={incident}
                        approveIncident={_props.approveIncident}
                        forbidIncident={_props.forbidIncident}
                    />
                })}

            </tbody>
        </table>;
    }
}