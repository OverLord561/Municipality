import * as React from 'react';
import autobind from 'autobind-decorator';


interface IInnerProps {
    totalCount: number;
    //pagination: (page: number) => void;
    listOfPages: number[];
    page: number;
    totalPages: number;
}

export default class Pagination extends React.Component<IInnerProps, any> {
    constructor(props: any) {
        super(props);
    }

    @autobind
    pagination(event: React.FormEvent<HTMLAnchorElement>) {
        event.currentTarget.className = 'active';
        //this.props.pagination(+event.currentTarget.dataset['page']);
    }

    @autobind
    renderPagination() {


        if (this.props.totalPages === 0) return <div />;
        const li = [];

        li.push(
            <li key={0} className={this.props.page === 1 ? 'active' : ''}>
                <a data-page={1} onClick={this.pagination}>
                    {1}
                </a>
            </li>,
        );

        if (
            this.props.listOfPages[0] !== 1 &&
            this.props.totalPages >= 4
        ) {
            li.push(
                <li className="disabled" key={-1}>
                    <a>...</a>
                </li>,
            );
        }

        for (let i = 0; i < this.props.listOfPages.length; i++) {
            if (this.props.listOfPages[i] !== 1) {
                li.push(
                    <li
                        key={this.props.listOfPages[i]}
                        className={
                            this.props.page === this.props.listOfPages[i]
                                ? 'active'
                                : ''
                        }
                    >
                        <a
                            data-page={this.props.listOfPages[i]}
                            onClick={this.pagination}
                        >
                            {this.props.listOfPages[i]}
                        </a>
                    </li>,
                );
            }
        }

        if (
            this.props.totalPages > 5 &&
            this.props.listOfPages[
            this.props.listOfPages.length - 1
            ] !== this.props.totalPages
        ) {
            li.push(
                <li className="disabled" key={-4}>
                    <a>...</a>
                </li>,
            );
        }

        //
        if (
            this.props.listOfPages[
            this.props.listOfPages.length - 1
            ] !== this.props.totalPages
        )
            li.push(
                <li
                    key={this.props.totalPages}
                    className={
                        this.props.page === this.props.totalPages
                            ? 'active'
                            : ''
                    }
                >
                    <a
                        data-page={this.props.totalPages}
                        onClick={this.pagination}
                    >
                        {this.props.totalPages}
                    </a>
                </li>,
            );

        return li;
    }

    render() {
        const _context = this;

        return <ul className="pagination" style={{color:'black'}}>
            <li className={
                this.props.page === 1 && 'disabled'
            }>
                <a
                    data-page={this.props.page - 1}

                    onClick={
                        this.props.page === 1
                            ? () => {
                                return false;
                            }
                            : this.pagination
                    }
                >
                    Previous
                </a>
            </li>
            {this.renderPagination()}
            <li className={
                this.props.page === this.props.totalPages &&
                'disabled'
            }>
                <a
                    data-page={this.props.page + 1}

                    onClick={
                        this.props.page === this.props.totalPages
                            ? () => {
                                return false;
                            }
                            : this.pagination
                    }
                >
                    Next
                </a>
            </li>
        </ul >;
    }
}
