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
  videoId: string;
  renewalPrice: number;
  title: string;
  description: string;
} & LessonEnrollment &
  (
    | {
        videoStatus: "Ready";
        videoOTP: {
          otp: string;
          playbackInfo: string;
        };
      }
    | {
        videoStatus: "NoVideo" | "Processing";
      }
  );
