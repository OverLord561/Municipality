import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import { IState, IPoint } from './logic/incidentsState';
import * as actions from './logic/incidentsActions';
import autobind from 'autobind-decorator';
import IncidentsDescription from './IncidentsDescription';
import Creation from "./Creation";

import MapContainer from './MapContainer';



type IProps = IState & RouteComponentProps<{}> & typeof dispatchProps;

const dispatchProps = {
    getIncidents: actions.GetIncidents,
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


            <MapContainer
                incidents={this.props.incidents}
                focusIncident={this.props.focusIncident}

            />
            <div className="col-lg-6 block ">
                <div className="row">
                    <div className="col-lg-4">
                        <Creation
                            createIncident={this.props.createIncident}
                        />
                    </div>
                    <div className="col-lg-8">
                        <IncidentsDescription
                            incidents={this.props.incidents}
                            focusIncident={this.props.focusIncident}
                        />
                                                
                    </div>
                </div>
            </div>

        </div>;
    }
}

function mapStateToProps(state: ApplicationState): IState {
    console.log('connect incidents');
    console.log(state);
    return {
        incidents: state.incidents.incidents
    };
};


export default connect(
    mapStateToProps, // Selects which state properties are merged into the component's props
    dispatchProps                 // Selects which action creators are merged into the component's props
)(Incidents) as typeof Incidents;