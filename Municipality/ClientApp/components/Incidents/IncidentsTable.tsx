import * as React from 'react';
import { connect } from 'react-redux';

import { IIncident } from './logic/incidentsState';
import * as actions from '../Incidents/logic/incidentsActions';
import { ApplicationState } from '../../store';

import IncidentDescription from './IncidentDescription';
import Pagination from './Pagination';


interface IStateProps {
    incidents: IIncident[];
    totalCount: number;
    listOfPages: number[];
    page: number;
    totalPages: number;
}

const dispatchProps = {
    focusIncident: actions.FocusIncident,
}

type Props = typeof dispatchProps & IStateProps;

class IncidentsTable extends React.Component<Props, any>{

    render() {
        return <div>
            {this.renderIncidentsTable()}
            {this.renderPagination()}
        </div>;


    };
    private renderPagination() {
        if (this.props.incidents.length === 0) return null;
        return <Pagination
            totalCount={this.props.totalCount}
            listOfPages={this.props.listOfPages}
            page={this.props.page}
            totalPages={this.props.totalPages}

        />

    }

    private renderIncidentsTable() {
        const context = this;

        return <table className='table'>
            <thead>
                <tr>
                    <th>Title</th>
                    <th>Adress</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {this.props.incidents.map(function (incident, index) {
                    return <IncidentDescription
                        key={index}
                        incident={incident}
                        focusIncident={context.props.focusIncident}
                    />
                })}

            </tbody>
        </table>;
    }
}


function mapStateToProps(state: ApplicationState): IStateProps {

    return {
        incidents: state.incidents.incidents,
        listOfPages: state.incidents.listOfPages,
        totalCount: state.incidents.totalCount,
        page: state.incidents.page,
        totalPages: state.incidents.totalPages,
    };
};

export default connect(
    mapStateToProps, // Selects which state properties are merged into the component's props
    dispatchProps                 // Selects which action creators are merged into the component's props
)(IncidentsTable);
