import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { IIncident } from './logic/incidentsState';
import IncidentDescription from './IncidentDescription';

interface IInnerProps {
    incidents: IIncident[];
    focusIncident: (id: number, value: boolean) => void;
}
export default class IncidentsDescription extends React.Component<IInnerProps, any>{

    render() {
        return <div>                
            {this.renderIncidentsTable()}
            {this.renderPagination()}
        </div>;

        
    };
    private renderPagination() {
        let prevStartDateIndex = (0 || 0) - 5;
        let nextStartDateIndex = (2 || 0) + 5;

        return <p className='clearfix text-center'>
            <Link className='btn btn-default pull-left' to={`/fetchdata/${prevStartDateIndex}`}>Previous</Link>
            <Link className='btn btn-default pull-right' to={`/fetchdata/${nextStartDateIndex}`}>Next</Link>
         
        </p>;
    }
    
    private renderIncidentsTable() {
        var _props = this.props;

        return <table className='table'>
            <thead>
                <tr>
                    <th>Title</th>
                    <th>Adress</th>
                </tr>
            </thead>
            <tbody>
                {this.props.incidents.map(function (incident, index) {
                    return <IncidentDescription
                        key={index}
                        incident={incident}
                        focusIncident={_props.focusIncident}
                    />
                })}

            </tbody>
        </table>;
    }



}