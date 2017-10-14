import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import { IState } from './logic/signUpState';
import * as actions from './logic/signUpActions';
import * as CounterStore from '../../store/Counter';

type IProps = IState & RouteComponentProps<{}> & typeof dispatchProps;

const dispatchProps = {
    authorize: actions.Authorize
}; 


class SignUp extends React.Component<IProps, any> {


    componentDidMount() {
        
    }

    public render() {
        //console.log(this.props.history.goBack())
        console.log(this.props)
        return <div>
            <input value={this.props.email} />
            <input value={this.props.password} />
            <input value={this.props.confirmPassword} />
            <input type="button" value="send" onClick={this.props.authorize} />

        </div>;
    }
}

function mapStateToProps(state: ApplicationState): IState {
    return {
        email: state.signUp.email,
        password: state.signUp.password,
        confirmPassword: state.signUp.confirmPassword
    };
};


export default connect(
    mapStateToProps, // Selects which state properties are merged into the component's props
    dispatchProps                 // Selects which action creators are merged into the component's props
)(SignUp) as typeof SignUp;





