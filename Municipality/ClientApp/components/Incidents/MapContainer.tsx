import * as React from "react";
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { IIncident } from './logic/incidentsState';
import Incident from './Incident';

import GoogleMap from 'google-map-react';

const AnyReactComponent = ({ text }: any) => <div className="elem">{text}</div>;

const Dnipropetrovsk: any = { lat: 48.460861, lng: 35.056737 };

interface IInnerProps {
    incidents: IIncident[];
}

export default class MapContainer extends React.Component<IInnerProps, any> {
    constructor(props: IInnerProps) {

        super(props);
        this.state = {
            center: Dnipropetrovsk,
            zoom: 15
        };
    }
    render() {

        const K_MARGIN_TOP = 30;
        const K_MARGIN_RIGHT = 30;
        const K_MARGIN_BOTTOM = 30;
        const K_MARGIN_LEFT = 30;        

        return <div  className="col-lg-6 block">
            <GoogleMap
                bootstrapURLKeys={{
                    key: "AIzaSyAxVVyh7rh6kKCYhxyWZSY_xkDNZ4YIK3k",
                    language: 'uk'
                }}

                defaultCenter={this.state.center}
                defaultZoom={this.state.zoom}
                margin={[K_MARGIN_TOP, K_MARGIN_RIGHT, K_MARGIN_BOTTOM, K_MARGIN_LEFT]}


            >
                {this.props.incidents.map(function (incident, index) {

                    return <Incident
                        key={index}
                        lat={incident.lat}
                        lng={incident.lng}
                        incident={incident}
                    />
                })}

            </GoogleMap >
        </div>

    }
}
