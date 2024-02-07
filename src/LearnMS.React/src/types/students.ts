export type Student = {
  id: string;
  email: string;
  profilePicture: string;
  fullName: string;
  phoneNumber: string;
  parentPhoneNumber: string;
  isVerified: boolean;
  schoolName: string;
  credit: number;
  level: "Level0" | "Level1" | "Level2" | "Level3";
};
