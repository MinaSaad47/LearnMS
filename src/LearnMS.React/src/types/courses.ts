import { Enrollment } from "@/types/enrollment";
import { Exam } from "./exams";
import { Lecture } from "./lectures";

export type CourseDetails = Course & {
  items: (Lecture | Exam)[];
};

export type Course = {
  id: string;
  title: string;
  level: "Level0" | "Level1" | "Level2" | "Level3";
  description: string;
  imageUrl: string;
  isPublished?: boolean;
  price: number;
  renewalPrice: number;
  expirationDays: number;
} & Enrollment;
