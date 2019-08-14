import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import { IState, IPoint, IIncident } from './logic/incidentsState';
import * as actions from './logic/incidentsActions';
import autobind from 'autobind-decorator';
import IncidentsTable from './IncidentsTable';
import Creation from "./Creation";

import MapContainer from './MapContainer';

interface IStateProps {
    incidents: IIncident[];
}


type IProps = IStateProps & RouteComponentProps<{}> & typeof dispatchProps;

const dispatchProps = {
    getIncidents: actions.RequestIncidents,
    createIncident: actions.CreateIncidents,
    focusIncident: actions.FocusIncident
};


class Incidents extends React.Component<IProps, any> {

    constructor(props: IProps) {
        super(props);
    }

    componentDidMount() {
        this.props.getIncidents();
    }


    public render() {
        return <div className="row">
            <button onClick={this.props.getIncidents} value='test'>clisk me</button>
           
            <div className="col-lg-6 block ">
                <div className="row">
                    <div className="col-lg-4">
                        <Creation
                            createIncident={this.props.createIncident}
                        />
                    </div>
                </div>
            </div>

        </div>;
    }
}

function mapStateToProps(state: ApplicationState): IStateProps {

    return {
        incidents: state.incidents.incidents
    };
};


export default connect(
    mapStateToProps, // Selects which state properties are merged into the component's props
    dispatchProps                 // Selects which action creators are merged into the component's props
)(Incidents) as typeof Incidents;