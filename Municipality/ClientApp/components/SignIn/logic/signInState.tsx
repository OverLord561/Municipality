
export interface IState {
    email: string;
    password: string;
    rememberMe: boolean;
}


export function getInitialState(): IState{

    return {
        email: "yurapuk452@gmail.com",
        password: "123Qaz-",
        rememberMe: false
    }
}