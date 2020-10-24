import { UserType } from '../models/user.model';

export interface UserResponseData {
    id: number,
    firstName: string,
    lastName: string,
    emailAddress: string,
    password: string,
    type: UserType
}
