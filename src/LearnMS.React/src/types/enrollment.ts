export type Enrollment =
  | {
      enrollment: "Expired";
      expiresAt: Date;
    }
  | {
      enrollment: "Active";
      expiresAt: Date;
    }
  | {
      enrollment: "NotEnrolled";
    };
