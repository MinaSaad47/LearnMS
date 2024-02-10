import { Enrollment } from "@/types/enrollment";
import { Lesson } from "./lessons";
import { Quiz } from "./quiz";

export type Lecture = {
  type: "Lecture";
  order: number;
  id: string;
  imageUrl: string;
  title: string;
  description: string;
  price: number;
  renewalPrice: number;
  isPublished?: boolean;
  expirationDays: number;
} & Enrollment;

export type LectureDetails = Lecture & {
  items: (Quiz | Lesson)[];
};
