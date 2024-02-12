export type CreditCode = {
  code: string;
  value: number;
  status: "Fresh" | "Sold" | "Redeemed";
  generatedAt: string;
  generator?: {
    id: string;
    email: string;
  };
  soldAt?: string;
  seller?: {
    id: string;
    email: string;
  };
  redeemer?: {
    id: string;
    email: string;
    redeemedAt: string;
  };
};
