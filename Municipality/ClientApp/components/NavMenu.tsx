import * as React from 'react';
import { NavLink, Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import autobind from 'autobind-decorator';
import * as actions from './SignIn/logic/signInActions';


import { ApplicationState } from '../store';

interface IStateProps {
    authorized?: boolean;
    userName?: string;
}


const dispatchProps = {
    logOut: actions.LogOut
};

type IProps = typeof dispatchProps & IStateProps;

export default class NavMenu extends React.Component<any, {}> {
    constructor(props: IProps) {
        super(props);
    }

    @autobind
    LogOut() {
        this.props.logOut();
    };

    public render() {
        return <div className='main-nav'>
            <div className='navbar navbar-inverse'>
                <div className='navbar-header'>
                    <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                        <span className='sr-only'>Toggle navigation</span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                    </button>
                    <Link className='navbar-brand' to={'/'}>Municipality

                    </Link>
                </div>
                <div className='clearfix'></div>
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav'>
                        <li>
                            <NavLink exact to={'/'} activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Home
                            </NavLink>
                        </li>


                        <li>
                            <NavLink to={'/incidents'} activeClassName='active'>
                                <span className='glyphicon glyphicon-warning-sign'></span> Incidents
                            </NavLink>
                        </li>

                        <li>
                            <NavLink to={'/sign-up'} activeClassName='active'>
                                <span className='glyphicon glyphicon-share-alt'></span> Sign Up
                            </NavLink>
                        </li>

                        <li>
                            <NavLink to={'/admin'} activeClassName='active'>
                                <span className='glyphicon glyphicon-calendar'></span> Admin Panel
                            </NavLink>
                        </li>


                        <li>
                            <NavLink to={'/sign-in'} activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Sign In
                            </NavLink>
                        </li>


                        <li >
                            <NavLink to={'/sign-out'} >
                                <span className='glyphicon glyphicon-log-out'></span> Log out
                                </NavLink>
                        </li>


                    </ul>
                </div>
            </div>
        </div>;
    }
}

//function mapStateToProps(state: ApplicationState): IStateProps {
//    console.log(state.signIn)
//    return {
//        authorized: state.signIn.authorized,
//        userName: state.signIn.userName
//    };
//};


//export default connect(
//    mapStateToProps, // Selects which state properties are merged into the component's props
//    dispatchProps                 // Selects which action creators are merged into the component's props
//)(NavMenu) as typeof NavMenu;