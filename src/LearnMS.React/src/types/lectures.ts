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
  enrollment: "Expired" | "Active" | "NotEnrolled" | undefined;
  isPublished?: boolean;
  expirationDays: number;
  expiresAt?: Date;
};

export type LectureDetails = Lecture & {
  items: (Quiz | Lesson)[];
};
