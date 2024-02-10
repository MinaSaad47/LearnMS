export type Lesson = {
  type: "Lesson";
  id: string;
  order: number;
  title: string;
};

type LessonEnrollment =
  | {
      enrollment: "Active";
      videoUrl: string;
      expiresAt: number;
    }
  | {
      enrollment: "NotEnrolled";
    }
  | {
      enrollment: "Expired";
      expiresAt: number;
    };

export type LessonDetails = {
  id: string;
  expirationHours: number;
  renewalPrice: number;
  title: string;
  description: string;
  videoSrc: string;
} & LessonEnrollment;
