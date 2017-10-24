import * as React from 'react';
import { IState, IIncident } from './logic/adminState';
import Incident from './Incident';

interface IInnerProps {
    getNotApproved: () => void;
    notApprovedIncidents: IIncident[];
}


export default class NotApproved extends React.Component<IInnerProps, any>{
    componentDidMount() {
        this.props.getNotApproved();
    }
    render() {
        
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
                {this.props.notApprovedIncidents.map(function (incident, index) {
                    return <Incident
                        key={index}
                        incident={incident}                        
                    />
                })}

            </tbody>
        </table>;
    }
}