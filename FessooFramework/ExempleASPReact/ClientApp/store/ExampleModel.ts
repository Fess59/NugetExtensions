import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';

//REDUX
export interface ExampleModelState {
    isLoading: boolean;
    startDateIndex?: number;
    models: ExampleModel[];
}

//MODEL ADAPTER
export interface ExampleModel {
    title: string;
    description: number;
}

//ACTIONS
interface RequestGetAllAction {
    type: 'Request_Get_All';
    startDateIndex: number;
}

interface ReceiveGetAllAction {
    type: 'Receive_Get_All';
    models: ExampleModel[];
    startDateIndex: number;
}


//ACTION SWITCH
type KnownAction2 = RequestGetAllAction | ReceiveGetAllAction;

export const actionCreators = {
    //Запрос всех моделей
    requestAll: (startDateIndex: number): AppThunkAction<KnownAction2> => (dispatch, getState) => {
        if (startDateIndex !== getState().exampleModels.startDateIndex) {
            //Формируем запрос
            let exampleModelTask = fetch(`api/ExampleDataApi/Index`)
                .then(response => response.json() as Promise<ExampleModel[]>)
                .then(data => {
                    dispatch({ type: 'Receive_Get_All', models: data, startDateIndex: startDateIndex });
                });
            ///Добавляем запрос
            addTask(exampleModelTask);
            ///Отправляем запрос ???
            dispatch({ type: 'Request_Get_All', startDateIndex: startDateIndex });
        }
    }
}

//REDUCER - state changed
const defaultState: ExampleModelState = { models: [], isLoading: false};

export const reducer: Reducer<ExampleModelState> = (state: ExampleModelState, incomingAction: Action) => {
    const action = incomingAction as KnownAction2;
    switch (action.type) {
        case 'Request_Get_All':
            return {
                models: state.models,
                isLoading: true,
                startDateIndex: action.startDateIndex
            };
        case 'Receive_Get_All':
            if (action.startDateIndex === state.startDateIndex) {
                return {
                    models: action.models,
                    isLoading: false,
                    startDateIndex: action.startDateIndex
                };
            }
            break;
        default:
            const exhaustiveCheck: never = action;
    }

    return state || defaultState;
};
