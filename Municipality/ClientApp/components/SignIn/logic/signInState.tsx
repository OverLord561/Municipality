
export interface IState {
    email: string;
    password: string;
    rememberMe: boolean;
    authorized: boolean;
    userName: string;
}


export function getInitialState(): IState{

    return {
        email: "yurapuk452@gmail.com",
        password: "123Qaz-",
        rememberMe: false,
        authorized: false,
        userName:""
    }
}