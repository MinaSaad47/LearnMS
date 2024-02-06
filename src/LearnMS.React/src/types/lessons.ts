export type Lesson = {
  type: "Lesson";
  id: string;
  order: number;
  title: string;
};

export type LessonDetails = {
  id: string;
  title: string;
  description: string;
  videoSrc: string;
};
