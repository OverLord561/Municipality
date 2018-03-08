import * as React from 'react';
import { IIncident } from '../Incidents/logic/incidentsState';
import { Carousel as Gallery } from 'react-responsive-carousel';
require('react-responsive-carousel/lib/styles/carousel.css');

interface IInnerProps {
    incident: IIncident;
}

export default class Carousel extends React.Component<IInnerProps, any>{


    render() {
        return <Gallery showArrows={true}>

            {this.props.incident.filePaths.map((path, index) => {
                return <div key={index}>
                    <img src={path} />
                </div>
            })}

            
        </Gallery>;
        //return <div id="myCarousel" className="carousel slide" data-ride="carousel">

        //    <ol className="carousel-indicators">
        //        {this.props.incident.filePaths.length > 1 &&
        //            this.props.incident.filePaths.map((path, index) => {
        //                return <li data-target="#myCarousel" data-slide-to={index} className={index === 0 && "active"} key={index}></li>;
        //            })}
        //    </ol>



        //    <div className="carousel-inner">


        //        {this.props.incident.filePaths.map((path, index) => {
        //            return <div className={index === 0 ? "item active" : "item"} key={index} >
        //                <img src={path} alt="/" style={{ width: "100%" }} />
        //            </div>;
        //        })}

        //        {this.props.incident.filePaths.length > 1 &&

        //            <a className="left carousel-control" href="#myCarousel" data-slide="prev">
        //                <span className="glyphicon glyphicon-chevron-left"></span>
        //                <span className="sr-only">Previous</span>
        //            </a>
        //        }

        //        {this.props.incident.filePaths.length > 1 &&

        //            <a className="right carousel-control" href="#myCarousel" data-slide="next">
        //                <span className="glyphicon glyphicon-chevron-right"></span>
        //                <span className="sr-only">Next</span>
        //            </a>
        //        }
        //    </div>
        //</div>;
    }
};