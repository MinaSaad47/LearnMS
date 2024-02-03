import { Lesson } from "./lessons";
import { Quiz } from "./quiz";

export type Lecture = {
  type: "lecture";
  order: number;
  id: string;
  name: string;
  price: number;
  renewPrice?: number;
};

export type LectureDetails = {
  id: string;
  name: string;
  price: number;
  renewPrice: number;
  items: (Quiz | Lesson)[];
};
