import React, { useCallback } from 'react'
import { useDropzone } from 'react-dropzone'
import PropTypes from 'prop-types';
import * as XLSX from 'xlsx';
import axios from 'axios';
import CloudUpload from '@material-ui/icons/CloudUpload';
import withAuth from './withAuth.js';

function MyDropzone() {
    const onDrop = useCallback(acceptedFiles => {
        const reader = new FileReader()

        reader.onabort = () => console.log('La lecture du fichier a été annulé')
        reader.onerror = () => console.log('La lecture du fichier a échoué')
        reader.onload = (evt) => {
            // Do whatever you want with the file contents
            
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
            const rembo = {
                DateRembourssement: new Date(),
            }
           /* axios.post('/api/Remboussement', rembo)
                .then(res => {
                    if (res.status == 201) {
                        alert('telechargement fichier avec succes');
                    }
                    else
                        alert('Echec de telechargement du fichier');
                })
            console.log(data_arr);
           /* for (var i = 1; i < data_arr.length-1; i++) {
                const data_ar = data_arr[i].split(",");
               
                const rembo2 = {
                    listId: ,
                    rembourssementId: data_ar[0],
                    dossierID: data_ar[2],
                    montant: data_ar[3],
                } 
               
               /* axios.post('/api/list', rembo2)
                    .then(res => {
                        if (res.status == 201) {
                            this.setState({
                              
                            });
                        }
                        
                    })
            }*/
           
            axios.post('/api/Remboussement', rembo)
                .then(res => {
                    if (res.status == 201) {
                        alert('telechargement fichier avec succes');
                        for (var i = 1; i < data_arr.length - 1; i++) {
                            const data_ar = data_arr[i].split(",");
                            const rembo2 = {
                                /*rembouse: res.data.rembourssementId,
                                dossier: data_ar[0],*/
                                RembouserembourssementId: res.data.rembourssementId,
                                Dossierreferance: data_ar[0],
                                montant: data_ar[1],
                            }
                            console.log('hi');
                            axios.post('/api/list', rembo2)
                                .then(res => {
                                    console.log(res.data);                            
                                    if (res.status == 201) {
                                       
                                    }
                                })
                            if (i == 2) {
                                fetch('api/list')
                                    .then(response => response.json())
                                    .then(data => {
                                        console.log('teste');
                                        console.log(data);
                                        console.log('teste');

                                    });
                            }
                        } 
                      
                    }
                });
            
            }
            
       

        acceptedFiles.forEach(file => reader.readAsBinaryString(file))
    }, [])
    const { getRootProps, getInputProps, isDragActive } = useDropzone({ onDrop })

    return (
        <div {...getRootProps()} style={{ fontSize: "20px", textAlign: "center", border: "3px solid black", height: "80vh", borderStyle: "dashed", borderColor: "rgba(0, 0, 0, 0.54)" }}>
            <input {...getInputProps()} />
            <CloudUpload style={{ fontSize: "100px", color: "rgba(0, 0, 0, 0.54)", marginTop: "25vh" }} />
            <p style={{ color: "rgba(0, 0, 0, 0.54)" }}>Glissez ou selectionnez un fichier </p>
        </div>
    )
}

MyDropzone.propTypes = {
    classes: PropTypes.object.isRequired,
};

export default withAuth(MyDropzone);
