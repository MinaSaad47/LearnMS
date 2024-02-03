import { Exam } from "./exams";
import { Lecture } from "./lectures";

export type Course = {
  id: string;
  name: string;
  description: string;
  coverUrl: string;
  price: number;
  renewPrice: number;
};

export type CourseDetails = {
  id: string;
  name: string;
  description: string;
  coverUrl: string;
  price: number;
  renewPrice: number;
  items: (Lecture | Exam)[];
};
