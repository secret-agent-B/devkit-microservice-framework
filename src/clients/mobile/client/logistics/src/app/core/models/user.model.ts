export enum UserType{
    Administratior,
    Seller,
    Driver,
    User
}

export class User {
    constructor(
        public id: number,
        public firstName: string,
        public lastName: string,
        public emailAddress: string,
        public password: string,
        public type: UserType
    ) {}
}
