
export interface IState {
    email: string;
    password: string;
    
}


export function getInitialState(): IState{

    return {
        email: "yurapuk452@gmail.com",
        password: "123Qaz-"
    }
}