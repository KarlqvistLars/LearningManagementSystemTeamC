export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  roleId: string;
  roleName: string;
  isActive: boolean;
}

export interface UpdateUserData {
  email: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string | null;
  phoneNumber: string | null;
  address: string | null;
  postalCode: string | null;
  city: string | null;
  roleId: string;
  isActive: boolean;
}
