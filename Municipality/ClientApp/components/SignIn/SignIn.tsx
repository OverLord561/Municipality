import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import { IState } from './logic/signInState';
import * as actions from './logic/signInActions';
import * as CounterStore from '../../store/Counter';
import autobind from 'autobind-decorator';

type IProps = IState & RouteComponentProps<{}> & typeof dispatchProps;

const dispatchProps = {
    authorize: actions.Authorize
};


class SignIn extends React.Component<IProps, IState> {
    constructor(props: IProps) {
        super(props);
        this.state = {
            password: this.props.password,
            email: this.props.email
        }
    }

   

    @autobind
    SetEmail(event: React.FormEvent<HTMLInputElement>) {
        this.setState({
            email: event.currentTarget.value
        });
    }

    @autobind
    SetPassword(event: React.FormEvent<HTMLInputElement>) {
        this.setState({
            password: event.currentTarget.value
        });
    }

    

    @autobind
    SubmitForm(event: any) {
        event.preventDefault();
        this.props.authorize(this.state);
    }

    public render() {
        return <form onSubmit={this.SubmitForm}>
            <input value={this.state.email} onChange={this.SetEmail} />
            <input value={this.state.password} onChange={this.SetPassword} />
            <input type="submit" value="send" />

        </form>;
    }
}

function mapStateToProps(state: ApplicationState): IState {
    return {
        email: state.signUp.email,
        password: state.signUp.password
    };
};


export default connect(
    mapStateToProps, // Selects which state properties are merged into the component's props
    dispatchProps                 // Selects which action creators are merged into the component's props
)(SignIn) as typeof SignIn;





