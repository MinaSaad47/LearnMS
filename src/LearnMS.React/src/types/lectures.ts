import { Lesson } from "./lessons";
import { Quiz } from "./quiz";

export type Lecture = {
  type: "Lecture";
  order: number;
  id: string;
  imageUrl: string;
  title: string;
  price: number;
  renewalPrice?: number;
  isExpired?: boolean;
  expiresAt?: Date;
};

export type LectureDetails = {
  id: string;
  title: string;
  price: number;
  renewPrice: number;
  isExpired?: boolean;
  expiresAt?: Date;
  imageUrl: string;
  description: string;
  items: (Quiz | Lesson)[];
};
