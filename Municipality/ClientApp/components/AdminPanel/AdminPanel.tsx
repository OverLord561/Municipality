import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import { IState, IIncident } from './logic/adminState';
import * as actions from './logic/adminActions';
import autobind from 'autobind-decorator';
import NotApproved from './NotApproved';

interface IStateProps {
    notApprovedIncidents: IIncident[];
}

type IProps = IState & RouteComponentProps<{}> & typeof dispatchProps;

const dispatchProps = {
    getIncidents: actions.GetIncidents
};

class AdminPanel extends React.Component<IProps, any> {
    constructor(props: IProps) {
        super();
    }
    public render() {
       
        return <div>

            <ul className="nav nav-tabs">
                <li className="active"><a data-toggle="tab" href="#home">New incidents</a></li>
                <li><a data-toggle="tab" href="#menu1">Menu 1</a></li>
                <li><a data-toggle="tab" href="#menu2">Menu 2</a></li>
            </ul>

            <div className="tab-content">
                <div id="home" className="tab-pane fade in active">
                    <NotApproved
                        getNotApproved={this.props.getIncidents}
                        notApprovedIncidents={this.props.notApprovedIncidents}
                    />
                </div>
                <div id="menu1" className="tab-pane fade">
                    <h3>Menu 1</h3>
                    <p>Some content in menu 1.</p>
                </div>
                <div id="menu2" className="tab-pane fade">
                    <h3>Menu 2</h3>
                    <p>Some content in menu 2.</p>
                </div>
            </div>

        </div>;
    }
}

function mapStateToProps(state: ApplicationState): IStateProps {
    
    return {
        notApprovedIncidents: state.admin.notApprovedIncidents
    };
};

export default connect(
    mapStateToProps, // Selects which state properties are merged into the component's props
    dispatchProps                 // Selects which action creators are merged into the component's props
)(AdminPanel) as typeof AdminPanel;