import React, { useCallback } from 'react'
import { useDropzone } from 'react-dropzone'
import PropTypes from 'prop-types';
import * as XLSX from 'xlsx';
import axios from 'axios';
import withAuth from '../withAuth.js';
import CloudUpload from '@material-ui/icons/CloudUpload';
import { confirmAlert } from 'react-confirm-alert'; // Import
import 'react-confirm-alert/src/react-confirm-alert.css'; 

function Emplo() {
    const onDrop = useCallback(acceptedFiles => {
        const reader = new FileReader()
        reader.onabort = () => console.log('file reading was aborted')
        reader.onerror = () => console.log('file reading has failed')
        reader.onload = (evt) => {
            console.log(evt);
            const bstr = evt.target.result;
            const wb = XLSX.read(bstr, { type: 'binary' });
            /* Get first worksheet */
            const wsname = wb.SheetNames[0];
            const ws = wb.Sheets[wsname];
            /* Convert array of arrays */
            const data = XLSX.utils.sheet_to_csv(ws, { header: 1 });
            /* Update state */
            console.log(data);
            const data_arr = data.split("\n");
     
            //const Auth = new AuthHelperMethods();

                        for (var i = 1; i < data_arr.length - 1; i++) {
                            const data_ar = data_arr[i].split(",");
                            console.log(data_ar);
                            const rembo2 = {
                                'matricule': data_ar[4],
                                'first_name': data_ar[6],
                                'last_name': data_ar[5],
                                'company': '',
                                'placeplacdeid': 1,
                                //'UserId': Auth.getProfile().unique_name
                            }

                            switch(data_ar[2]) {
                                case "VIVO ENERGY MAROC": rembo2.company = "VEM"; break;
                                case "SHELL ET VIVO LUBRIFIANTS DU MAROC": rembo2.company = "SVL"; break;
                                case "VIVO ENERGY AFRICA SERVICES": rembo2.company = "VEAS"; break;
                                case "SHELL ET VIVOLUB AFRICA SERVICE (SVLAS)": rembo2.company = "SVLAS"; break;
                                default: rembo2.company = "";
                            }
                           /* console.log-(rembo2) */
                            if(rembo2.company != "") {
                            axios.post('/api/employees', rembo2)
                                .then(res => {
                                    console.log(res.  data);
                                    if (res.status == 201) {
                                       // console.log(rembo2.matricule + " has been added");
                                    }
                                })
                            }
                        }

                        confirmAlert({
                            title: 'Information',
                            message: 'L\'importation des données terminé',
                            buttons: [
                              {
                                label: 'Ok!',
                                onClick: () => {}
                              }]
                            })

        }
        acceptedFiles.forEach(file => reader.readAsArrayBuffer(file))
    }, [])
    const { getRootProps, getInputProps, isDragActive } = useDropzone({ onDrop })

    return (
        <div {...getRootProps()} style={{ fontSize: "20px", textAlign: "center", border: "3px solid black", height: "80vh", borderStyle: "dashed", borderColor: "rgba(0, 0, 0, 0.54)" }}>
            <input {...getInputProps()} />
            <CloudUpload style={{ fontSize: "100px", color: "rgba(0, 0, 0, 0.54)", marginTop: "25vh"  }} />
            <p style={{  color:"rgba(0, 0, 0, 0.54)"  }}>Glissez ou selectionnez un fichier </p>
        </div>
    )
}

Emplo.propTypes = {
    classes: PropTypes.object.isRequired,
};

export default withAuth(Emplo);
