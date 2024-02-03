// CoverPicker.tsx

import axios, { CancelTokenSource } from "axios";
import React, { useCallback, useEffect, useState } from "react";
import { useDropzone } from "react-dropzone";
import { Progress } from "./ui/progress";

interface CoverPickerProps {
  defaultCover?: string;
  onCoverSelect: (cover: string) => void;
}

const CoverPicker: React.FC<CoverPickerProps> = ({
  onCoverSelect,
  defaultCover,
}) => {
  const [selectedCover, setSelectedCover] = useState<any>(defaultCover);
  const [uploadProgress, setUploadProgress] = useState<number>(0);
  const [cancelTokenSource, setCancelTokenSource] =
    useState<CancelTokenSource | null>(null);

  const onDrop = useCallback(
    async (acceptedFiles: File[]) => {
      if (acceptedFiles && acceptedFiles.length > 0) {
        const coverImage = acceptedFiles[0];

        // Create a cancel token source for handling cancellation
        const cancelToken = axios.CancelToken.source();
        setCancelTokenSource(cancelToken);

        try {
          const formData = new FormData();
          formData.append("cover", coverImage);

          setUploadProgress(0);
          setSelectedCover(null);

          const response = await axios.post(
            "http://localhost:3001/upload",
            formData,
            {
              onUploadProgress: (progressEvent) => {
                const progress = Math.round(
                  (progressEvent.loaded / (progressEvent.total ?? 1)) * 100
                );
                setUploadProgress(progress);
              },
              cancelToken: cancelToken.token,
            }
          );

          if (response.status === 200) {
            const imageUrl = response.data;
            setSelectedCover(imageUrl);
            onCoverSelect(imageUrl);
          } else {
            console.error("Failed to upload the image");
          }
        } catch (error) {
          if (!axios.isCancel(error)) {
            console.error("Error uploading image:", error);
          }
        } finally {
          // Reset the cancel token source and upload progress
          setCancelTokenSource(null);
          setUploadProgress(0);
        }
      }
    },
    [onCoverSelect]
  );

  const { getRootProps, getInputProps, isDragActive } = useDropzone({ onDrop });

  useEffect(() => {
    // Cancel the ongoing request if the component is unmounted
    return () => {
      if (cancelTokenSource) {
        cancelTokenSource.cancel("Component unmounted");
      }
    };
  }, [cancelTokenSource]);

  return (
    <div
      {...getRootProps()}
      className={`cover-picker ${
        isDragActive ? "drag-active" : ""
      } w-full h-full  rounded-lg flex items-center justify-center`}>
      <input {...getInputProps()} />
      {selectedCover ? (
        <img
          src={selectedCover}
          alt='Cover'
          className='object-cover w-full h-full rounded-lg'
        />
      ) : (
        <div className='flex flex-col items-center gap-2'>
          <p>Drag & drop a cover image here, or click to select one</p>
          {uploadProgress > 0 && (
            <Progress value={uploadProgress} className='w-[60%]' />
          )}
        </div>
      )}
    </div>
  );
};

export default CoverPicker;
