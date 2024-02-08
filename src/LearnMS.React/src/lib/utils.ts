import { clsx, type ClassValue } from "clsx";
import { twMerge } from "tailwind-merge";

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

export function validateEgyptianPhoneNumber(phoneNumber?: string) {
  if (!phoneNumber) return false;
  // Regular expression for Egyptian phone numbers
  var regex = /^(012|011|010|015)\d{8}$/;

  // Remove any white spaces from the input
  phoneNumber = phoneNumber.replace(/\s/g, "");

  return regex.test(phoneNumber);
}

export function getFirstCharacters(string: string) {
  const [firstName, lastName] = string.split(" ");

  return firstName?.[0] + lastName?.[0];
}
