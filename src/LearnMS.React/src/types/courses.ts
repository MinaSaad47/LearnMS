import { Exam } from "./exams";
import { Lecture } from "./lectures";

export type CourseDetails = Course & {
  items: (Lecture | Exam)[];
};

export type Course = {
  id: string;
  title: string;
  description: string;
  isExpired: boolean;
  expiresAt?: Date;
  imageUrl: string;
  price: number;
  renewalPrice: number;
};
