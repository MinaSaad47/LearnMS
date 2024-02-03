import Uppy from "@uppy/core";
import Tus from "@uppy/tus";

import { useEffect, useState } from "react";

import Dashboard from "@uppy/dashboard";
import DropTarget from "@uppy/drop-target";

import "@uppy/core/dist/style.min.css";
import "@uppy/dashboard/dist/style.min.css";
import "@uppy/drop-target/dist/style.css";

const FilesPage = () => {
  const [uppy] = useState(() =>
    new Uppy().use(Tus, {
      endpoint: "/api/files",
      headers: {
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
    })
  );

  useEffect(() => {
    uppy
      .use(DropTarget, {
        target: "#files-drop-zone",
        onDrop: () => {
          const plugin: any = uppy.getPlugin("Dashboard");
          plugin.openModal();
        },
      })
      .use(Dashboard, {
        inline: false,
        target: "#files-drop-zone",
        height: 200,
      });
  }, []);

  return (
    <div className='w-full h-full' id='files-drop-zone'>
      <h1>Files</h1>
    </div>
  );
};

export default FilesPage;
