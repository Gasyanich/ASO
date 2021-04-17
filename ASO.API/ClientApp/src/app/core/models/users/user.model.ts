import { Role } from './role.model';

export class User {
  id: number;
  firstName: string;
  lastName: string;
  patronymic: string;
  email: string;
  phoneNumber: string;
  role: Role;
  sex: Sex;
  sexName: string;
}

export enum Sex {
  Male = 0,
  Female = 1
}
