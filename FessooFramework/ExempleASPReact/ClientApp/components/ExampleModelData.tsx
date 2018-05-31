import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as ExampleModelState from '../store/ExampleModel';

// At runtime, Redux will merge together...
type ExampleModelProps =
    ExampleModelState.ExampleModelState        // ... state we've requested from the Redux store
    & typeof ExampleModelState.actionCreators      // ... plus action creators we've requested
    & RouteComponentProps<{ startDateIndex: string }>; // ... plus incoming routing parameters

class ExampleModelData extends React.Component<ExampleModelProps, {}> {
    componentWillMount() {
        // This method runs when the component is first added to the page
        let startDateIndex = parseInt(this.props.match.params.startDateIndex) || 0;
        this.props.requestAll(startDateIndex);
    }

    componentWillReceiveProps(nextProps: ExampleModelProps) {
        // This method runs when incoming props (e.g., route params) change
        let startDateIndex = parseInt(nextProps.match.params.startDateIndex) || 0;
        this.props.requestAll(startDateIndex);
    }

    public render() {
        return <div>
            <h1>Example descriptions</h1>
            <p>Test data list.</p>
            {this.renderTable()}
            {this.renderPagination()}
        </div>;
    }

    private renderTable() {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Title</th>
                    <th>Description</th>
                </tr>
            </thead>
            <tbody>
                {this.props.models.map(model =>
                    <tr key={model.title}>
                        <td>{model.title}</td>
                        <td>{model.description}</td>
                    </tr>
                )}
            </tbody>
        </table>;
    }

    private renderPagination() {
        //let prevStartDateIndex = (this.props.startDateIndex || 0) - 5;
        //let nextStartDateIndex = (this.props.startDateIndex || 0) + 5;

        return <p className='clearfix text-center'>
            <Link className='btn btn-default pull-left' to={`/examplemodeldata`}>Previous</Link>
            <Link className='btn btn-default pull-right' to={`/examplemodeldata`}>Next</Link>
            {this.props.isLoading ? <span>Loading...</span> : []}
        </p>;
    }
}

export default connect(
    (state: ApplicationState) => state.exampleModels, // Selects which state properties are merged into the component's props
    ExampleModelState.actionCreators                 // Selects which action creators are merged into the component's props
)(ExampleModelData) as typeof ExampleModelData;
