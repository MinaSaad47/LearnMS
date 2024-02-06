export type CreditCode = {
  code: string;
  value: number;
  status: "Fresh" | "Sold" | "Redeemed";
  redeemer: {
    id: string;
    email: string;
  };
  generator: {
    id: string;
    email: string;
  };
};
