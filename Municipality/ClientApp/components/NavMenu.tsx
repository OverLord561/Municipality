import * as React from 'react';
import { NavLink, Link } from 'react-router-dom';
import { connect } from 'react-redux';

import { ApplicationState } from '../store';

interface IStateProps {
    authorized?: boolean;
    userName?: string;
}

class NavMenu extends React.Component<IStateProps, {}> {
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
                    <Link className='navbar-brand' to={'/'}>Municipality <br /> <label className="hello">Hello, <strong>{this.props.userName}</strong></label></Link>
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
                            <NavLink to={'/counter'} activeClassName='active'>
                                <span className='glyphicon glyphicon-education'></span> Counter
                            </NavLink>
                        </li>

                        <li>
                            <NavLink to={'/incidents'} activeClassName='active'>
                                <span className='glyphicon glyphicon-warning-sign'></span> Incidents
                            </NavLink>
                        </li>

                        <li>
                            <NavLink to={'/fetchdata'} activeClassName='active'>
                                <span className='glyphicon glyphicon-th-list'></span> Fetch data
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={'/sign-up'} activeClassName='active'>
                                <span className='glyphicon glyphicon-share-alt'></span> Sign Up
                            </NavLink>
                        </li>

                        {!this.props.authorized &&
                            <li>
                                <NavLink to={'/sign-in'} activeClassName='active'>
                                    <span className='glyphicon glyphicon-home'></span> Sign In
                            </NavLink>
                            </li>
                        }
                        {this.props.authorized &&
                            < li >
                            <NavLink to={'/sign-up'} activeClassName='active'>
                                <span className='glyphicon glyphicon-log-out'></span> Log out
                                </NavLink>
                            </li>
                        }

                    </ul>
                </div>
            </div>
        </div>;
    }
}

function mapStateToProps(state: ApplicationState): IStateProps {
    console.log(state.signIn)
    return {
        authorized: state.signIn.authorized,
        userName: state.signIn.userName
    };
};


export default connect(
    mapStateToProps, // Selects which state properties are merged into the component's props
    {}                 // Selects which action creators are merged into the component's props
)(NavMenu) as typeof NavMenu;