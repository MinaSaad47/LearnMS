import ForgotPasswordModal from "@/pages/auth/forgot-password-modal";
import AddAssistantModal from "@/pages/dashboard/assistants/add-assistant-modal";
import UpdateAssistantModal from "@/pages/dashboard/assistants/update-assistant-modal";
import AddCreditModal from "@/pages/dashboard/students/add-credit-modal";
import AddStudentModal from "@/pages/dashboard/students/add-student-modal";
import ProfileModal from "@/pages/student/profile/profile-modal";
import { useModalStore } from "@/store/use-modal-store";
import React from "react";

// eslint-disable-next-line @typescript-eslint/no-explicit-any
const modals: Record<string, React.FC<any>> = {
  "add-assistant-modal": AddAssistantModal,
  "add-student-modal": AddStudentModal,
  "update-assistant-modal": UpdateAssistantModal,
  "add-credit-modal": AddCreditModal,
  "profile-modal": ProfileModal,
  "forgot-password-modal": ForgotPasswordModal,
};

const ModalProvider = () => {
  const { data, type, onClose } = useModalStore();

  const Modal = modals[type];

  if (!Modal) {
    return null;
  }

  // eslint-disable-next-line @typescript-eslint/ban-ts-comment
  // @ts-ignore
  return <Modal {...data} onClose={onClose} />;
};

export default ModalProvider;
