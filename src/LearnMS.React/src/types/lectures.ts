import { Lesson } from "./lessons";
import { Quiz } from "./quiz";

export type Lecture = {
  type: "Lecture";
  order: number;
  id: string;
  imageUrl: string;
  title: string;
  price: number;
  renewalPrice: number;
  isExpired?: boolean;
  expirationDays: number;
  expiresAt?: Date;
};

export type LectureDetails = {
  id: string;
  title: string;
  expirationDays: number;
  price: number;
  renewalPrice: number;
  isExpired?: boolean;
  expiresAt?: Date;
  imageUrl: string;
  description: string;
  items: (Quiz | Lesson)[];
};
