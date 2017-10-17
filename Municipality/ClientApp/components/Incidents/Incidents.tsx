import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import { IState, IPoint } from './logic/incidentsState';
import * as actions from './logic/incidentsActions';
import autobind from 'autobind-decorator';

import Creation from "./Creation";

import MapContainer from './MapContainer';

import './Incidents.css';

type IProps = IState & RouteComponentProps<{}> & typeof dispatchProps;

const dispatchProps = {
    getIncidents: actions.GetIncidents,
    createIncident: actions.CreateIncidents
};


class Incidents extends React.Component<IProps, any> {


    componentDidMount() {
        this.props.getIncidents();
    }


    public render() {
        return <div className="row">
           

            <MapContainer
                incidents={this.props.incidents}
            />

            <Creation
                createIncident={this.props.createIncident}
            />

        </div>;
    }
}

function mapStateToProps(state: ApplicationState): IState {
    return {
        incidents: state.incidents.incidents       
    };
};


export default connect(
    mapStateToProps, // Selects which state properties are merged into the component's props
    dispatchProps                 // Selects which action creators are merged into the component's props
)(Incidents) as typeof Incidents;