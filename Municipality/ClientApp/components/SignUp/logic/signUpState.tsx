
export interface IState {
    email: string;
    password: string;
    confirmPassword: string;
}


export function getInitialState(): IState{

    return {
        email: "yurapuk452@gmail.com",
        password: "123Qaz-",
        confirmPassword: "123Qaz-"
    }
}