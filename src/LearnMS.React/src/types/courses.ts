import { Exam } from "./exams";
import { Lecture } from "./lectures";

export type CourseDetails = Course & {
  items: (Lecture | Exam)[];
};

export type Course = {
  id: string;
  title: string;
  description: string;
  enrollment: "Expired" | "Active" | "NotEnrolled" | undefined;
  expiresAt?: Date;
  imageUrl: string;
  isPublished?: boolean;
  price: number;
  renewalPrice: number;
  expirationDays: number;
};
