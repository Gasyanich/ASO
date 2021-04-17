import {Role} from './role.model';

export class User {
  id: number;
  firstName: string;
  lastName: string;
  patronymic: string;
  email: string;
  phoneNumber: string;
  role: Role;
  sex: 0 | 1;
  sexName: string;
}
