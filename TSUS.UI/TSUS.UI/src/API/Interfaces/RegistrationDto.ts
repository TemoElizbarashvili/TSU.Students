export interface RegistrationDto {
    email: string;
    userName: string;
    password: string;
    profilePicture: Uint8Array;
    departmentId: number;
  }